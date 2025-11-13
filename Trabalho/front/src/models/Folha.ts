export default interface Folha{
    id? : number,
    nome : string,
    cpf : string,
    mes : number,
    ano : number,
    horasTrabalhadas : number,
    valorHora : number,
    salarioBruto? : number,
    impostoRenda? : number,
    inss? : number,
    fgts? : number,
    salarioLiquido? : number


}