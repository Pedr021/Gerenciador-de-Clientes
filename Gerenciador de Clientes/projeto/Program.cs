using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadordeCliente
{
     class Program
    {
        [System.Serializable]
         struct Cliente
        {
            public string nome;
            public string email;
            public string cpf;
        }

        static List<Cliente> clientes = new List<Cliente>();

        enum Menu {listagem = 1 , Adicionar = 2 , Remover = 3, Sair = 4}

        static void Main(string[] args)
        {
            Carregar();
            bool escolheusair = false;
            while (!escolheusair)
            {
                Console.WriteLine("Sistemas de clientes - Bem vindo!");
                Console.WriteLine("1-Listagem\n2-Adicionar\n3-Remover\n4-Sair");
                int intop = int.Parse(Console.ReadLine());
                Menu opcao = (Menu)intop;

                switch (opcao)
                {
                    case Menu.Adicionar:
                        Adicionar();
                        break;
                    case Menu.listagem:
                        listagem();
                        break;
                    case Menu.Remover:
                        Remover();
                        break;
                    case Menu.Sair:
                        escolheusair = true;
                        break;
                } 
                Console.Clear();    
            }
        }
           
        static void Adicionar()
        {
            Cliente cliente = new Cliente();
            Console.WriteLine("Cadastro de cliente:");
            Console.WriteLine("Nome do cliente:");
            cliente.nome = Console.ReadLine();
            Console.WriteLine("Email do cliente:");
            cliente.email = Console.ReadLine();
            Console.WriteLine("CPF do cliente:");
            cliente.cpf = Console.ReadLine();

            clientes.Add(cliente);
            Salvar();

            Console.WriteLine("Cadastro concluido,aperte Enter para sair. ");
            Console.ReadLine();

        }
        static void listagem()
        {
            if(clientes.Count>0)
            {
                Console.WriteLine("Lista de cliente:");
                int i = 0;
                foreach (Cliente cliente in clientes)
                {
                    Console.WriteLine($"ID:{i}");
                    Console.WriteLine($"Nome:{cliente.nome}");
                    Console.WriteLine($"E-mail:{cliente.email}");
                    Console.WriteLine($"CPF:{cliente.cpf}");
                    Console.WriteLine("=======================");
                    i++;

                }

            }
            else
            {
                Console.WriteLine("Nenhum cliente Cadastrado!");
            }
            Console.WriteLine("Aperte enter para sair");
            Console.ReadLine();
        }
        static void Remover()
        {
            listagem();
            Console.WriteLine("Digite o ID do cliente que voce quer remover:");
            int id = int.Parse(Console.ReadLine());
            if(id >= 0 && id < clientes.Count)
            {
                clientes.RemoveAt(id);
                Salvar();
            }
            else
            {
                Console.WriteLine("ID digitado é invalido, tente novamente!");
                Console.ReadLine();
            }
        }


        static void Salvar()
        {
            FileStream stream = new FileStream("Clients.dat", FileMode.OpenOrCreate);
            BinaryFormatter encoder = new BinaryFormatter();

            encoder.Serialize(stream, clientes);

            stream.Close();
        }

        static void Carregar()
        {
            FileStream stream = new FileStream("Clients.dat", FileMode.OpenOrCreate);

            try
            {            
                BinaryFormatter encoder = new BinaryFormatter();

                clientes = (List<Cliente>)encoder.Deserialize(stream);


                if (clientes ==null)
                {
                  clientes = new List<Cliente>();
                }
                
            }
            catch (Exception e)
            {

                clientes = new List<Cliente>();

            }
            stream.Close();


        }
    }
}
    