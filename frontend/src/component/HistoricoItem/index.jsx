import React from 'react';
import './index.css';

const HistoricoItem = ({ locacao, aoClicar }) => {
  return (
    <div className="item-locacao" onClick={aoClicar}>
      <div><strong>Modelo:</strong> {locacao.modeloVeiculo}</div>
      <div><strong>Data Retirada:</strong> {new Date(locacao.dataRetirada).toLocaleDateString()}</div>
      <div><strong>Data Devolução:</strong> {new Date(locacao.dataDevolucao).toLocaleDateString()}</div>
      <div><strong>Valor Pago:</strong> R$ {locacao.valorPago.toFixed(2)}</div>
    </div>
  );
};

export default HistoricoItem;
