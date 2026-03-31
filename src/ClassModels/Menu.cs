using System.Globalization;

namespace ClassModels
{
    /// <summary>
    /// Model to represent a menu registered by a user.
    /// </summary>
    public class Menu
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Menu"/> class.
        /// </summary>
        /// <param name="menuId">The menu identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="date">The menu date.</param>
        public Menu(int menuId, int userId, DateTime date)
        {
            this.MenuId = menuId;
            this.UserId = userId;
            this.Date = date;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Menu"/> class from CSV data.
        /// </summary>
        /// <param name="menuData">The menu data.</param>
        public Menu(string[] menuData)
        {
            if (menuData.Length < 3)
            {
                throw new ArgumentException("Invalid menu data. Expected 3 elements.");
            }

            this.MenuId = int.Parse(menuData[0], CultureInfo.InvariantCulture);
            this.UserId = int.Parse(menuData[1], CultureInfo.InvariantCulture);
            this.Date = DateTime.ParseExact(menuData[2], "dd-MM-yyyy", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Gets or sets the menu identifier.
        /// </summary>
        public int MenuId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the menu date.
        /// </summary>
        public DateTime Date { get; set; }
    }
}
