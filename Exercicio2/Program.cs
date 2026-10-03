using System.Text.Json;

string json = File.ReadAllText("estoque.json");

DadosDeEstoque? dados = JsonSerializer.Deserialize<DadosDeEstoque>(json, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});

if (dados == null) {
    Console.WriteLine("Não foi possível ler os dados do estoque.");
    return;
}

Console.WriteLine("--- PRODUTOS DISPONÍVEIS ---");

foreach (Produto produto in dados.Estoque){
    Console.WriteLine($"Código: {produto.CodigoProduto} | Produto: {produto.DescricaoProduto} | Estoque: {produto.Estoque}");
}

int proximoIdMovimentacao = 1;
bool continuar = true;

while (continuar) {
    Console.WriteLine("\nDigite o código do produto:");
    int codProduto = int.Parse(Console.ReadLine()!);
    

    Produto? prodSelecionado = dados.Estoque.Find(
        produto => produto.CodigoProduto == codProduto
    );

    if (prodSelecionado == null)
    {
        Console.WriteLine("Produto não encontrado.");
        continue;
    }

    Console.WriteLine($"Produto selecionado: {prodSelecionado.DescricaoProduto}");

    Console.WriteLine("Digite o tipo da movimentação (1 - Entrada / 2 - Saída):");
    int tipoMovimentacao = int.Parse(Console.ReadLine()!);

    Console.WriteLine("Digite a quantidade:");
    int quantidade = int.Parse(Console.ReadLine()!);
    if (quantidade <= 0) {
        Console.WriteLine("A quantidade deve ser maior que zero.");
        continue;
    }

    string descricao;

    if (tipoMovimentacao == 1) {
        prodSelecionado.Estoque += quantidade;
        descricao = "Entrada";
    } else if (tipoMovimentacao == 2) {
        if (quantidade > prodSelecionado.Estoque) {
            Console.WriteLine("Estoque insuficiente para realizar a saída.");
            continue;
        }
        prodSelecionado.Estoque -= quantidade;
        descricao = "Saída";
    } else {
        Console.WriteLine("Tipo de movimentação inválido.");
        continue;
    }

    MovimentacaoDeEstoque movimentacao = new MovimentacaoDeEstoque {
        Id = proximoIdMovimentacao,
        Descricao = descricao,
        Quantidade = quantidade
    };

    proximoIdMovimentacao++;

    Console.WriteLine("\n--- MOVIMENTAÇÃO REALIZADA ---");
    Console.WriteLine($"Id: {movimentacao.Id}");
    Console.WriteLine($"Tipo: {movimentacao.Descricao}");
    Console.WriteLine($"Produto: {prodSelecionado.DescricaoProduto}");
    Console.WriteLine($"Quantidade movimentada: {movimentacao.Quantidade}");
    Console.WriteLine($"Estoque final: {prodSelecionado.Estoque}");

    Console.WriteLine("\nDeseja realizar outra movimentação? (1 - Sim / 2 - Não)");
    int opcao = int.Parse(Console.ReadLine()!);

    if (opcao == 2) {
        continuar = false;
    }
}