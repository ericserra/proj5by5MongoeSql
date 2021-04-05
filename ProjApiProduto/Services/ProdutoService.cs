using System.Collections.Generic;
using MongoDB.Driver;
using ProjApiProduto.Models;
using ProjApiProduto.Utils;

namespace ProjApiProduto.Services
{
    public class ProdutoService
    {
        private readonly IMongoCollection<Produto> _produtos;
        public ProdutoService(IProjMongoDotnetDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _produtos = database.GetCollection<Produto>(settings.ProdutoCollectionName);
        }

        public List<Produto> Get() =>
            _produtos.Find(produto => true).ToList();

        public Produto Get(string id) =>
            _produtos.Find<Produto>(produto => produto.Id == id).FirstOrDefault();

        public Produto Create(Produto produto)
        {
            _produtos.InsertOne(produto);
            return produto;
        }

        public void Update(string id, Produto produtoIn) =>
            _produtos.ReplaceOne(produto => produto.Id == id, produtoIn);

        public void Remove(Produto clienteIn) =>
            _produtos.DeleteOne(cliente => cliente.Id == clienteIn.Id);

        public void Remove(string id) => 
            _produtos.DeleteOne(cliente => cliente.Id == id);
    }
}