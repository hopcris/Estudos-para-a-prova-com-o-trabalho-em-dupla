import { useState } from "react";
import Folha from "../../../models/Folha";
import axios from "axios";

// http://localhost:5085

function CadastrarFolha(){

    const [nome, setNome] = useState("");
    const [cpf, setCpf] = useState("");
    const [mes, setMes] = useState(0);
    const [ano, setAno] = useState(0);
    const [horasTrabalhadas, setHorasTrabalhadas] = useState(0);
    const [valorHora, setValorHora] = useState(0);


    function enviarFolha(event : any){

        event.preventDefault();
        submeterFolhaAPI();

    }

    async function submeterFolhaAPI() {
        
        try{
            const folha: Folha = {
                nome, cpf, mes, ano, horasTrabalhadas, valorHora,
            };
            const resposta = await axios.post("http://localhost:5085/api/folha/cadastrar", folha);
            console.log( await resposta.data);

        }catch (error : any){
            if(error.status === 409){
                console.log("Essa folha ja foi cadastrada!");
            }
        }
    }

    return (

        <div>
            <h1>Cadastrar folha de pagamento</h1>
            <form onSubmit={enviarFolha}>
                <div>
                    <label>Nome:</label>
                    <input onChange={(e : any) => setNome(e.target.value)} type="text" />
                </div>
                <div>
                    <label>CPF:</label>
                    <input onChange={(e : any) => setCpf(e.target.value)} type="text" />
                </div>
                <div>
                    <label>Mes:</label>
                    <input type="number" onChange={(e: any) => setMes(parseInt(e.target.value))} />
                </div>
                <div>
                    <label>Ano:</label>
                    <input type="number" onChange={(e : any) => setAno(parseInt(e.target.value))} />
                </div>
                <div>
                    <label>Horas trabalhadas:</label>
                    <input type="number" onChange={(e : any) => setHorasTrabalhadas(parseInt(e.target.value))} />
                </div>
                <div>
                    <label>Valor da hora:</label>
                    <input type="number" onChange={(e : any) => setValorHora(parseFloat(e.target.value))} />
                </div>
                <div>
                    <button type="submit">Cadastrar</button>
                </div>
            </form>
        </div>

    );   
}

export default CadastrarFolha;