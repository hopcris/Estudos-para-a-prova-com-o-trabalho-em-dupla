using System;

namespace Larissa.Models;

public class FolhaPagamento
{

    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public int Mes { get; set; }
    public int Ano { get; set; }
    public int HorasTrabalhadas { get; set; }
    public double ValorHora { get; set; }
    public double SalarioBruto { get; set; }
    public double ImpostoRenda { get; set; }
    public double Inss { get; set; }
    public double Fgts { get; set; }
    public double SalarioLiquido { get; set; }
    
    


}
