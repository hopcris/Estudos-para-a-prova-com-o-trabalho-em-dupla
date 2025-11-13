import React from "react";
import ListarFolhas from "./components/pages/folha/ListarFolhas";
import CadastrarFolha from "./components/pages/folha/CadastrarFolha";
import Home from "./components/pages/Home";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { Link } from "react-router-dom";

function App() {
  return (
    <div id="app">
      <h1>Projeto para calculo e cadastro de folhas de pagamento</h1>
      <div>
        <BrowserRouter>
          <nav>
            <ul>
              <li>
                <Link to="/folha/cadastrar">Cadastrar folhas</Link>
              </li>
              <li>
                <Link to="/folha/listar">Listar folhas</Link>
              </li>
            </ul>
          </nav>
          <Routes>
            <Route path="/folha/cadastrar" element={<CadastrarFolha/>} />
            <Route path="/folha/listar" element={<ListarFolhas/>} />
          </Routes>

          <footer>
            Rodape
          </footer>
        </BrowserRouter>
      </div>
    </div>
  );
}

export default App;
