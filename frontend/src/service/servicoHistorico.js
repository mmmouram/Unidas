export const listarHistorico = async (usuarioId, dataInicio = '', dataFim = '') => {
  try {
    let url = `/api/historicolocacao?usuarioId=${usuarioId}`;
    if (dataInicio && dataFim) {
      url += `&dataInicio=${dataInicio}&dataFim=${dataFim}`;
    }
    const resposta = await fetch(url);
    if (!resposta.ok) {
      throw new Error('Erro ao buscar histórico de locações.');
    }
    return await resposta.json();
  } catch (error) {
    console.error('listarHistorico erro:', error);
    throw error;
  }
};

export const obterDetalhes = async (id) => {
  try {
    const resposta = await fetch(`/api/historicolocacao/${id}`);
    if (!resposta.ok) {
      throw new Error('Erro ao obter os detalhes da locação.');
    }
    return await resposta.json();
  } catch (error) {
    console.error('obterDetalhes erro:', error);
    throw error;
  }
};
