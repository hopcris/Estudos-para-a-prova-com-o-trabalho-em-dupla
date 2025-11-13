using Larissa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Context>();

builder.Services.AddCors(options =>
    options.AddPolicy("Acesso Total",
        configs => configs
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod())
);

var app = builder.Build();

app.MapGet("/", () => "API de folha de pagamento");

//Funcionalidade 1
app.MapPost("/api/folha/cadastrar", async ([FromBody] FolhaPagamento folhaPagamento, [FromServices] Context ctx) =>
{

    if (folhaPagamento.Mes > 12 || folhaPagamento.Mes <= 0) return Results.BadRequest("Mês inválido!!");
    if (folhaPagamento.Ano < 2000) return Results.BadRequest("Ano inválido!!");

    FolhaPagamento? resultado = ctx.FolhasPagamentos.FirstOrDefault(f => f.Cpf == folhaPagamento.Cpf
                                                                    && f.Mes == folhaPagamento.Mes
                                                                    && f.Ano == folhaPagamento.Ano);
    if (resultado is not null) return Results.BadRequest("Folha de pagamento já cadastrado!!");

    folhaPagamento.SalarioBruto = folhaPagamento.HorasTrabalhadas * folhaPagamento.ValorHora;

    if (folhaPagamento.SalarioBruto <= 1903.98)
    {
        folhaPagamento.ImpostoRenda = 0;
    }
    else if (folhaPagamento.SalarioBruto >= 1903.99 && folhaPagamento.SalarioBruto <= 2826.65)
    {
        folhaPagamento.ImpostoRenda = (folhaPagamento.SalarioBruto * 0.075) - 142.80;
    }
    else if (folhaPagamento.SalarioBruto >= 2826.66 && folhaPagamento.SalarioBruto <= 3751.05)
    {
        folhaPagamento.ImpostoRenda = (folhaPagamento.SalarioBruto * 0.15) - 354.80;
    }
    else if (folhaPagamento.SalarioBruto >= 3751.06 && folhaPagamento.SalarioBruto <= 4664.68)
    {
        folhaPagamento.ImpostoRenda = (folhaPagamento.SalarioBruto * 0.225) - 636.13;
    }
    else if(folhaPagamento.SalarioBruto > 4664.68)
    {
        folhaPagamento.ImpostoRenda = (folhaPagamento.SalarioBruto * 0.275) - 869.36;
    }


    if (folhaPagamento.SalarioBruto <= 1693.72)
    {
        folhaPagamento.Inss = folhaPagamento.SalarioBruto * 0.08;
    }
    else if (folhaPagamento.SalarioBruto <= 2822.90)
    {
        folhaPagamento.Inss = folhaPagamento.SalarioBruto * 0.09;
    }
    else if (folhaPagamento.SalarioBruto <= 5645.80)
    {
        folhaPagamento.Inss = folhaPagamento.SalarioBruto * 0.11;
    }
    else
    {
        folhaPagamento.Inss = 621.03;
    }

    folhaPagamento.Fgts = folhaPagamento.SalarioBruto * 0.08;

    folhaPagamento.SalarioLiquido = folhaPagamento.SalarioBruto - folhaPagamento.ImpostoRenda - folhaPagamento.Inss;

    ctx.Add(folhaPagamento);
    await ctx.SaveChangesAsync();

    return Results.Created($"/folhaPagamento/{folhaPagamento.Id}", folhaPagamento);

});

//Funcionalidade 2
app.MapGet("api/folha/listar", async ([FromServices] Context ctx) =>
{

    if (ctx.FolhasPagamentos.Count() == 0)
    {
        return Results.NotFound("Nenhuma folha cadastrada!!");
    }

    var folhas = await ctx.FolhasPagamentos.ToListAsync();

    return Results.Ok(folhas);

});

//Funcionalidade 3
app.MapGet("/api/folha/buscar/{cpf}/{mes}/{ano}", async ([FromRoute] string cpf
                                                      , [FromRoute] int mes
                                                      , [FromRoute] int ano
                                                      , [FromServices] Context ctx) =>
{
    var folha = await ctx.FolhasPagamentos
                         .Where(p => p.Cpf.Contains(cpf) && p.Mes == mes && p.Ano == ano)
                         .ToListAsync();

    if (folha.Count() == 0)
    {
        return Results.NotFound("Folha de pagamento não encontrada!!");
    }

    return Results.Ok(folha);

});

//Funcionalidade 4
app.MapDelete("/api/folha/remover/{cpf}/{mes}/{ano}", async ( [FromRoute] string cpf,
                                                              [FromRoute] int mes,
                                                              [FromRoute] int ano,
                                                              [FromServices] Context ctx) =>{

    FolhaPagamento? folha = await ctx.FolhasPagamentos
        .FirstOrDefaultAsync(f => f.Cpf == cpf && f.Ano == ano && f.Mes == mes);

    if (folha is null)
        return Results.NotFound("Folha não encontrada");

    ctx.FolhasPagamentos.Remove(folha);
    await ctx.SaveChangesAsync();

    return Results.Ok("Folha removida com sucesso!!");
});


//Funcionalidade 5
app.MapGet("/api/folha/total-liquido", async ([FromServices] Context ctx) =>
{
    var folhas = await ctx.FolhasPagamentos.ToListAsync();

    double total = 0;

    if (folhas.Count() == 0) return Results.NotFound("Nenhuma folha cadastrada!!");

    foreach (var f in ctx.FolhasPagamentos)
    {
        total += f.SalarioLiquido;
    }

    var resultado = new
    {
        totalLiquido = Math.Round(total, 2)
    };
    return Results.Ok(resultado);
});

app.UseCors("Acesso Total");

app.Run();



