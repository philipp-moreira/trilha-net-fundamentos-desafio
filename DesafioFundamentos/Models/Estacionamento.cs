using System.Text.RegularExpressions;

namespace DesafioFundamentos.Models;

public class Estacionamento : IEstacionamento
{
    public const int TAMANHO_INICIAL_LISTA_VEICULOS = 1;
    private readonly decimal precoInicial = 0M;
    private readonly decimal precoPorHora = 0M;
    private readonly List<string> veiculos;
    private readonly Regex patternPlacaVeiculo;


    public Estacionamento(decimal precoInicial, decimal precoPorHora, Regex patternPlacaVeiculo)
    {
        this.precoInicial = precoInicial;
        this.precoPorHora = precoPorHora;
        this.veiculos = new List<string>(TAMANHO_INICIAL_LISTA_VEICULOS);
        this.patternPlacaVeiculo = patternPlacaVeiculo;
    }


    public decimal PrecoInicial { get; }
    public decimal PrecoPorHora { get; }
    public Regex PatternPlacaVeiculo { get; }


    /// <inheritdoc/>
    public virtual void AdicionarVeiculo()
    {
        Console.WriteLine("Digite a placa do veículo para estacionar:");
        var placaVeiculoInformada = Console.ReadLine();

        if (!PlacaVeiculoValida(placaVeiculoInformada))
        {
            Console.WriteLine($"Valor informado, não atende ao padrão esperado '{this.patternPlacaVeiculo}'.");
            return;
        }

        HigienizarValorInformadoPlacaVeiculo(ref placaVeiculoInformada);

        if (PlacaVeiculoExiste(placaVeiculoInformada))
        {
            Console.WriteLine("Placa/veículo informada(o) já existe.");
            return;
        }

        this.veiculos.Add(placaVeiculoInformada);
    }

    /// <inheritdoc/>
    public virtual void RemoverVeiculo()
    {
        Console.WriteLine("Digite a placa do veículo para remover:");
        string placa = Console.ReadLine();

        // Consiste se o veículo existe
        if (!PlacaVeiculoExiste(placa))
        {
            Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
            return;
        }

        // Consiste a quantidade de horas informada
        if (!PegarQuantidadeHorasValida(out uint horas))
        {
            Console.WriteLine($"Quantidade de horas informada inválida.{Environment.NewLine}Era esperado um valor positivo e inteiro.");
            return;
        }

        try
        {
            checked
            {
                decimal valorTotal = precoInicial + (precoPorHora * horas);
                Console.WriteLine($"O veículo {placa} foi removido e o preço total foi de: R$ {valorTotal:F2}");
            }
        }
        catch (OverflowException ex)
        {
            Console.WriteLine($"Uma situação não esperada ocorreu e não foi possível remover e calcular a permanência do veículo '{placa}'.");
            Console.WriteLine($"Detalhes:{Environment.NewLine}{ex}.");
            return;
        }

        this.veiculos.Remove(placa);
    }

    /// <inheritdoc/>
    public virtual void ListarVeiculos()
    {
        // Verifica se há veículos no estacionamento
        if (!veiculos.Any())
        {
            Console.WriteLine("Não há veículos estacionados.");
            return;
        }

        Console.WriteLine("Os veículos estacionados são:");
        this.veiculos.ForEach(placaVeiculo => Console.WriteLine($"- {placaVeiculo}"));
    }

    /// <inheritdoc/>
    public virtual bool PlacaVeiculoExiste(string placaVeiculo)
    {
        return veiculos.Any(x => x.ToUpper() == placaVeiculo.ToUpper());
    }


    /// <summary>
    /// Consiste se uma placa/veículo é válida.
    /// </summary>
    /// <param name="placaVeiculo"></param>
    /// <returns></returns>
    protected internal virtual bool PlacaVeiculoValida(string placaVeiculo)
    {
        return patternPlacaVeiculo.IsMatch(placaVeiculo);
    }

    /// <summary>
    /// Pega a quantidade de horas que um veículo/placa ficou estacionado, garantindo que o valor informado seja um valor positivio.
    /// </summary>
    /// <param name="horas"></param>
    /// <returns></returns>
    protected internal virtual bool PegarQuantidadeHorasValida(out uint horas)
    {
        Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");
        var valor = Console.ReadLine();

        return uint.TryParse(valor, out horas);
    }

    /// <summary>
    /// Remove carácteres não esperados, para:
    /// <list type="bullet">
    /// <item>Espaços em branco no início e fim da placa/veículo.</item>
    /// </list>
    /// </summary>
    /// <param name="placaVeiculo"></param>
    protected internal static void HigienizarValorInformadoPlacaVeiculo(ref string placaVeiculo)
    {
        placaVeiculo = placaVeiculo.Trim();
    }
}
