using System;
using System.Linq;
using thy_arac_takip_sistemi_api.Models;

namespace thy_arac_takip_sistemi_api.Interfaces
{
    public interface IReaderModule
    {
        public POJO GetOrders(int id);

        //Update door states its contains reader module data and door states
        public POJO UpdateDoorStates(int id, string doorData);
        //Create reader module
        public POJO CreateReaderModule(ReaderModule readerModule);
        //Get reader with id 
        public ReaderModule GetReaderModule(int id);
        //Get all reader modules all
        public IQueryable<ReaderModule> GetReaderModulesAll { get; }
        //Delete reader via id
        POJO DeleteReaderModule(int id);

    }
}
