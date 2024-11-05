using BO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class JewelryDAO
    {   
        private SilverJewelry2023DbContext context;
        private static JewelryDAO instance;
        public static JewelryDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new JewelryDAO();
                }
                return instance;
            }
        }
        public JewelryDAO() 
        {
            context = new SilverJewelry2023DbContext();
        }

        public bool addJewelry(SilverJewelry silverJewelry)
        {
            bool result = false;
            SilverJewelry silver = this.GetSilverJewerly(silverJewelry.SilverJewelryId);
            if (silver == null)
            {
                try
                {
                    context.SilverJewelries.Add(silverJewelry);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                }
            }
            return result;
        }
        public bool updateJewelry(SilverJewelry silverJewelry)
        {
            bool result = false;
            SilverJewelry silver = this.GetSilverJewerly(silverJewelry.SilverJewelryId);
            if (silver != null)
            {
                try
                {
                    context.Entry(silver).CurrentValues.SetValues(silverJewelry);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                }
            }
            return result;
        }
        public bool deleteJewelry(string jewlryid)
        {
            bool result = false;
            SilverJewelry silver = this.GetSilverJewerly(jewlryid);
            if (silver != null)
            {
                try
                {
                    context.SilverJewelries.Remove(silver);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                }
            }
            return result;
        }

        private SilverJewelry GetSilverJewerly(string id)
        {
            return context.SilverJewelries.FirstOrDefault(m => m.SilverJewelryId.Equals(id));
        }
    }
}
