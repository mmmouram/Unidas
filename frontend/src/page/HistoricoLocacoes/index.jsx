import React, { useEffect, useState } from 'react';
import './index.css';
import { listarHistorico } from '../../service/servicoHistorico';
import HistoricoItem from '../../component/HistoricoItem';
import HistoricoDetalhesModal from '../../component/HistoricoDetalhesModal';

const HistoricoLocacoes = () => {
  // Usuário autenticado (valor fixo para exemplo ou obtido via contexto/autenticação real)
  const usuarioId = 1;

  const [locacoes, setLocacoes] = useState([]);
  const [dataInicio, setDataInicio] = useState('');
  const [dataFim, setDataFim] = useState('');
  const [locacaoDetalhe, setLocacaoDetalhe] = useState(null);
  const [modalAberto, setModalAberto] = useState(false);

  const carregarHistorico = async () => {
    try {
      const resposta = await listarHistorico(usuarioId, dataInicio, dataFim);
      setLocacoes(resposta);
    } catch (error) {
      console.error('Erro ao carregar o histórico:', error);
    }
  };

  useEffect(() => {
    carregarHistorico();
  }, []);

  const aplicarFiltro = () => {
    carregarHistorico();
  };

  const abrirDetalhes = (locacao) => {
    setLocacaoDetalhe(locacao);
    setModalAberto(true);
  };

  const fecharModal = () => {
    setModalAberto(false);
    setLocacaoDetalhe(null);
  };

  return (
    <div className="container-historico">
      <h1>Histórico de Locações</h1>
      <div className="filtro">
        <label>
          Data Início:
          <input
            type="date"
            value={dataInicio}
            onChange={(e) => setDataInicio(e.target.value)}
          />
        </label>
        <label>
          Data Fim:
          <input
            type="date"
            value={dataFim}
            onChange={(e) => setDataFim(e.target.value)}
          />
        </label>
        <button onClick={aplicarFiltro}>Aplicar Filtro</button>
      </div>
      <div className="lista-locacoes">
        {locacoes.map((locacao) => (
          <HistoricoItem
            key={locacao.id}
            locacao={locacao}
            aoClicar={() => abrirDetalhes(locacao)}
          />
        ))}
      </div>
      {modalAberto && locacaoDetalhe && (
        <HistoricoDetalhesModal
          locacao={locacaoDetalhe}
          onClose={fecharModal}
        />
      )}
    </div>
  );
};

export default HistoricoLocacoes;
