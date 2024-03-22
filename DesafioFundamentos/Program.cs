using System.Text.RegularExpressions;
using DesafioFundamentos.Models;

// Coloca o encoding para UTF8 para exibir acentuação
Console.OutputEncoding = System.Text.Encoding.UTF8;

var precoInicial = 0M;
var precoPorHora = 0M;
var padraoPlacaVeiculoEsperada = new Regex(@"\S\w{1,}", RegexOptions.IgnoreCase);

Console.WriteLine($"Seja bem vindo ao sistema de estacionamento!{Environment.NewLine}Digite o preço inicial:");
precoInicial = Convert.ToDecimal(Console.ReadLine());

Console.WriteLine("Agora digite o preço por hora:");
precoPorHora = Convert.ToDecimal(Console.ReadLine());

// Instancia a classe Estacionamento, já com os valores obtidos anteriormente
IEstacionamento es = new Estacionamento(precoInicial, precoPorHora, padraoPlacaVeiculoEsperada);

string opcao = string.Empty;
bool exibirMenu = true;

// Realiza o loop do menu
while (exibirMenu)
{
    Console.Clear();
    Console.WriteLine("Digite a sua opção:");
    Console.WriteLine("1 - Cadastrar veículo");
    Console.WriteLine("2 - Remover veículo");
    Console.WriteLine("3 - Listar veículos");
    Console.WriteLine("4 - Encerrar");

    var opcaoMenuInformada = Console.ReadLine();

    _ = Enum.TryParse(opcaoMenuInformada, out FuncionalidadesMenu opcaoMenu);

    switch (opcaoMenu)
    {
        case FuncionalidadesMenu.CadastrarVeiculo:
            es.AdicionarVeiculo();
            break;
        case FuncionalidadesMenu.RemoverVeiculo:
            es.RemoverVeiculo();
            break;
        case FuncionalidadesMenu.ListarVeiculos:
            es.ListarVeiculos();
            break;
        case FuncionalidadesMenu.Encerrar:
            exibirMenu = false;
            break;
        default:
            Console.WriteLine("Opção inválida");
            break;
    };

    Console.WriteLine("Pressione uma tecla para continuar");
    Console.ReadLine();
}

Console.WriteLine("O programa se encerrou");
