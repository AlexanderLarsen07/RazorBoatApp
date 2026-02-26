using System;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using SailClubLibrary.Helpers.Filter;
using SailClubLibrary.Helpers.Sorting;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Members
{
    public class IndexModel : PageModel
    {
        private IMemberRepository mRepo;

        public List<Member> Members { get; set; }
        [BindProperty]
        public Member Member { get; set; }
        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }
        [BindProperty(SupportsGet = true)]//
        public MemberType? SelectedMemberType { get; set; }
        [BindProperty(SupportsGet = true)]//
        public string FilterBy {get; set;} //


        public IndexModel(IMemberRepository memberRepository)
        {
            mRepo = memberRepository;
        }
        public void OnGet()
        {
            //Predicate<Member> predicate = b => b.TheMemberType == SelectedMemberType; // nope
            //Predicate<Member> predicate1 = b => b.SurName.Contains(FilterCriteria);
            //List<Predicate<Member>> predicates= new List<Predicate<Member>>();
            //predicates.Add(predicate);
            //predicates.Add(predicate1);

            Members = mRepo.GetAllMembers();
            if (!String.IsNullOrEmpty(FilterCriteria))
            {
                //Members = mRepo.FilterMembers(FilterCriteria);
                Members = mRepo.GetAllMembers();
                Members = MemberFilter(Members);
            }
            else
            {
                Members = mRepo.GetAllMembers();
            }
        }
        public IActionResult OnPost()
        {
            mRepo.RemoveMember(Member);
            return RedirectToPage("index");
        }
        private List<Member> MemberFilter(List<Member> members)
        {
            Predicate<Member> predicate = b => b.TheMemberType == SelectedMemberType;
            List<Predicate<Member>> predicates = new List<Predicate<Member>>();
            predicates.Add(predicate);
            switch (FilterBy)
            {
                case "All":
                    members = FilterFunctions<Member>.Filter(Members, predicates);
                    break;
                case "FirstName":
                    Predicate<Member> firstName = b => b.FirstName.Contains(FilterCriteria);
                    predicates.Add(firstName);
                    members = FilterFunctions<Member>.Filter(Members, predicates);
                    break;
                case "SurName":
                    Predicate<Member> surName = b => b.SurName.Contains(FilterCriteria);
                    predicates.Add(surName);
                    members = FilterFunctions<Member>.Filter(Members, predicates);
                    break;
                case "PhoneNumber":
                    Predicate<Member> phone = b => b.PhoneNumber.Contains(FilterCriteria);
                    predicates.Add(phone);
                    members = FilterFunctions<Member>.Filter(Members, predicates);
                    break;
                case "Mail":
                    Predicate<Member> mail = b => b.Mail.Contains(FilterCriteria);
                    predicates.Add(mail);
                    members = FilterFunctions<Member>.Filter(Members, predicates);
                    break;
                case "City":
                    Predicate<Member> city = b => b.City.Contains(FilterCriteria);
                    predicates.Add(city);
                    members = FilterFunctions<Member>.Filter(Members, predicates);
                    break;
                default:
                    break;
            }
            return members;
        }
    }
}
