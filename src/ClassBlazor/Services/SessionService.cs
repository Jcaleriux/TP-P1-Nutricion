using ClassModels;

namespace ClassBlazor.Services
{
    /// <summary>
    /// Stores the authenticated user for the current Blazor session.
    /// </summary>
    public class SessionService
    {
        /// <summary>
        /// Gets or sets the authenticated user associated with the current session.
        /// </summary>
        public User? CurrentUser { get; set; }
    }
}
