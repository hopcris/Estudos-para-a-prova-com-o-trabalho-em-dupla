import React from "react";
import { Link } from "react-router-dom";

function Home() {
  return (
    <div id="home">
      <h1>Bem-vindo ao Sistema de Produtos 🛍️</h1>
      <p>Gerencie seus produtos de forma simples e rápida!</p>

      <div className="botoes">
        <Link to="/produto/listar" className="botao">
            Listar Produtos
        </Link>
        <Link to="/produto/cadastrar" className="botao">
            Cadastrar Produto
        </Link>
      </div>
    </div>
  );
}

export default Home;
