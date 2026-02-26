using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Exceptions;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Bookings
{
    public class CreateBookingModel : PageModel
    {
        private IBookingRepository _repo;
        private IBoatRepository _boatRepo;
        private IMemberRepository _memberRepo;

        //[BindProperty]
        //public Booking Booking { get; set; }
        
        public Boat Boat { get; set; }
        [BindProperty]
        public string PhoneNumber { get; set; }
        //[BindProperty]
        //public Member Member { get; set; }
        [BindProperty]
        public DateTime StartDate { get; set; }
        [BindProperty]
        public DateTime EndDate { get; set; }
        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public string Destination { get; set; }
        //[BindProperty(SupportsGet = true)]
        //public Boat Boat { get; set; }
        public CreateBookingModel(IBoatRepository boatRepo, IBookingRepository bookingRepository, IMemberRepository memberRepository)
        {
            _repo = bookingRepository;
            _boatRepo = boatRepo;
            _memberRepo = memberRepository;
        }
        public void OnGet(string SailNumber)
        {
            Boat = _boatRepo.SearchBoat(SailNumber);

            //Booking = new Booking()
            //{
            //    TheBoat = _boatRepo.SearchBoat(SailNumber)
            //};
        }
        public IActionResult OnPost(string SailNumber)
        {
            ModelState.Remove("Boat");
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                DateTime Start = StartDate;
                DateTime End = EndDate;
                int id = Id;
                string destination = Destination;
                Member member = _memberRepo.SearchMember(PhoneNumber);
                Boat boat = _boatRepo.SearchBoat(SailNumber);
                Booking booking = new Booking(id, Start, End, destination, member, boat);
                _repo.AddBooking(booking);
            }
            catch (NullReferenceException nex)
            {
                ViewData["ErrorMessage"] = nex.Message;
                return Page();
            }
            catch (InvalidDataException iex)
            {
                ViewData["ErrorMessage"] = iex.Message;
            }
            catch (OverlappingDateException oex)
            {
                ViewData["ErrorMessage"] = oex.Message;
            }
            catch (Exception exc)
            {
                ViewData["ErrorMessage"] = exc.Message;
            }
            return RedirectToPage("index");
        }
    }
}