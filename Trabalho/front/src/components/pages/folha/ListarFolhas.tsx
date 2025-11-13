import { useEffect, useState } from "react";
import Folha from "../../../models/Folha";
import axios from "axios";

function ListarFolhas(){

    const [folhas, setFolhas] = useState<Folha[]>([]);

    useEffect(() => {
    console.log("O componente foi carregado!");
    buscarFolhasAPI();
  }, []);

    async function buscarFolhasAPI() {
        try {
        const resposta = await axios.get(
            "http://localhost:5085/api/folha/listar"
        );
        setFolhas(resposta.data);
        } catch (error) {
        console.log("Erro na requisição: " + error);
        }
    }
    
    async function deletarFolha(cpf : string, mes : number, ano : number) {

        try{
            const resposta = await axios.delete(`http://localhost:5085/api/folha/remover/${encodeURIComponent(cpf)}/${mes}/${ano}`);

            buscarFolhasAPI();
        } catch (error){
            console.log("Erro ao deletar a folha: " + error);
        }
        
    }

    return (

        <div id="listar_folhas">
            <h1>Listar folhas</h1>
            <table>
                <thead>
                    <tr>
                        <th>#</th>
                        <th>Nome</th>
                        <th>CPF</th>
                        <th>Mes</th>
                        <th>Ano</th>
                        <th>Horas <br /> trabalhadas</th>
                        <th>Valor hora</th>
                        <th>Salario bruto</th>
                        <th>Imposto Renda</th>
                        <th>Inss</th>
                        <th>Fgts</th>
                        <th>Salario liquido</th>
                    </tr>
                </thead>
                <tbody>
                    {folhas.map((folha) => (
                        <tr key={folha.id}>
                            <td>{folha.id}</td>
                            <td>{folha.nome}</td>
                            <td>{folha.cpf}</td>
                            <td>{folha.mes}</td>
                            <td>{folha.ano}</td>
                            <td>{folha.horasTrabalhadas}</td>
                            <td>{folha.valorHora}</td>
                            <td>{folha.salarioBruto}</td>
                            <td>{folha.impostoRenda}</td>
                            <td>{folha.inss}</td>
                            <td>{folha.fgts}</td>
                            <td>{folha.salarioLiquido}</td>
                            <td>
                                <button onClick={() => deletarFolha(folha.cpf, folha.mes, folha.ano)}>Deletar</button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}

export default ListarFolhas;