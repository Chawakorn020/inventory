using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace inventory.Pages.Rangsit
{
    public class CreateRangsitModel : PageModel
    {
		public StockInfo stockInfo = new StockInfo(); 
        public string errorMessage = "";
        public string successMessage = "";

		public void OnGet()
        {
        }
        public void OnPost()
        {
            stockInfo.item = Request.Form["item"];
            stockInfo.storeid = Request.Form["storeid"];
            stockInfo.supplier = Request.Form["supplier"];
            stockInfo.amount = Request.Form["amount"];

            if (stockInfo.item.Length == 0 || stockInfo.storeid.Length == 0 ||
                stockInfo.supplier.Length == 0 || stockInfo.amount.Length == 0)
            {
                errorMessage = "ALL the fields are required";
                return;
            }
            try
            {
                String connectionString = "Server=tcp:1660703602.database.windows.net,1433;Initial Catalog=cs436;Persist Security Info=False;User ID=Chawakorn;Password=Namo1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO stocks" +
                                 "(item, storeid, supplier,amount)VALUE" +
                                 "(@item, @storeid, @supplier, @amount);";
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@item", stockInfo.item);
                        command.Parameters.AddWithValue("@storeid", stockInfo.item);
                        command.Parameters.AddWithValue("@supplier", stockInfo.item);
                        command.Parameters.AddWithValue("@amount", stockInfo.item);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)

            {
                errorMessage = ex.Message;
                return;
            }
            stockInfo.item = "";
            stockInfo.storeid = "";
            stockInfo.supplier = "";
            stockInfo.amount = "";
            successMessage = "New Item Added Correctly";

            Response.Redirect("/Rangsit/IndexRangsit");
        }
    }
}
