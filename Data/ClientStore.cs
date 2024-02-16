using API_CRUD.Models;
using API_CRUD.Models.Dto;

namespace API_CRUD.Data
{
    public static class ClientStore
    {
        public static List<ClientDto> clientList = new List<ClientDto>
        {
                new ClientDto 
                {
                   
                 Id=1,Name="Client1",Address="Alger",PhoneNumber="0555555555",Email="Client1@outlook.fr",Order="Samsung 24 plus"
              
                },
                  new ClientDto
                {

                 Id=2,Name="Client2",Address="Blida",PhoneNumber="0777777777",Email="Client2@outlook.fr",Order="Pc Asus"

                }

        };
    }
}
