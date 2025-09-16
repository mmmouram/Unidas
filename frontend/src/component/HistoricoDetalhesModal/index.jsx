import React from 'react';
import './index.css';

const HistoricoDetalhesModal = ({ locacao, onClose }) => {
  return (
    <div className="modal-overlay">
      <div className="modal-conteudo">
        <button className="modal-fechar" onClick={onClose}>X</button>
        <h2>Detalhes da Locação</h2>
        <div><strong>Modelo:</strong> {locacao.modeloVeiculo}</div>
        <div><strong>Data Retirada:</strong> {new Date(locacao.dataRetirada).toLocaleDateString()}</div>
        <div><strong>Data Devolução:</strong> {new Date(locacao.dataDevolucao).toLocaleDateString()}</div>
        <div><strong>Local de Retirada:</strong> {locacao.localRetirada}</div>
        <div><strong>Local de Devolução:</strong> {locacao.localDevolucao}</div>
        <div><strong>Valor Pago:</strong> R$ {locacao.valorPago.toFixed(2)}</div>
        <div><strong>Status:</strong> {locacao.status}</div>
      </div>
    </div>
  );
};

export default HistoricoDetalhesModal;
