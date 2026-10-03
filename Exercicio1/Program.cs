using System.Text.Json;

string json = File.ReadAllText("dadosVendas.json");

DadosDeVendas? dados = JsonSerializer.Deserialize<DadosDeVendas>(json,new JsonSerializerOptions{PropertyNameCaseInsensitive = true});

if (dados == null)
{
    Console.WriteLine("Não foi possível ler os dados das vendas.");
    return;
}

Dictionary<string, decimal> comissoesPorVendedor = new();

foreach (Venda venda in dados.Vendas) {
    decimal comissao;

    if (venda.Valor < 100) {
        comissao = 0;
    } else if (venda.Valor < 500) {
        comissao = venda.Valor * 0.01m;
    } else {
        comissao = venda.Valor * 0.05m;
    }

    if (comissoesPorVendedor.ContainsKey(venda.Vendedor)) {
    comissoesPorVendedor[venda.Vendedor] += comissao;
    } else {
    comissoesPorVendedor[venda.Vendedor] = comissao;
    }
}

Console.WriteLine("\n--- COMISSÃO TOTAL POR VENDEDOR ---");

foreach (var vendedor in comissoesPorVendedor) {
    Console.WriteLine($"{vendedor.Key}: R$ {vendedor.Value:F2}");
}
