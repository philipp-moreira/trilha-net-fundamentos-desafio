namespace DesafioFundamentos.Models;

public interface IEstacionamento
{
    /// <summary>
    /// Adiciona um novo véiculo, que estacionou. Registrando apenas sua placa.
    /// </summary>
    public void AdicionarVeiculo();

    /// <summary>
    /// Realiza a remoção de um véiculo, realizando o cálculo para o seu tempo de permanência no estacionamento.
    /// </summary>
    /// <exception cref="System.OverflowException">Pode ocorrer caso o valor calculado venha a "estourar/exceder" o tipo de dado <see cref="decimal"/></exception>
    public void RemoverVeiculo();

    /// <summary>
    /// Lista a placa de todos os veículos estacionados.
    /// </summary>
    public void ListarVeiculos();

    /// <summary>
    /// Verifica se uma determinada placa/veículo está estacionado no estacionamento.
    /// </summary>
    /// <param name="placaVeiculo"></param>
    /// <returns></returns>
    public bool PlacaVeiculoExiste(string placaVeiculo);
}
