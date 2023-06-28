using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DogGo.Models;
using DogGo.Repositories;
using DogGo.Models.ViewModels;

namespace DogGo.Controllers
{
    public class WalksController : Controller
    {

        private readonly IWalkRepository _walkRepo;
        private readonly IDogRepository _dogRepo;
        private readonly IWalkerRepository _walkerRepo;
        private readonly IOwnerRepository _ownerRepo;
        private readonly INeighborhoodRepository _neighborhoodRepo;

        public WalksController(IWalkRepository walkRepository, IDogRepository dogRepository, IWalkerRepository walkerRepository, IOwnerRepository ownerRepository, INeighborhoodRepository neighborhoodRepository)
        {
            _walkRepo = walkRepository;
            _dogRepo = dogRepository;
            _walkerRepo = walkerRepository;
            _ownerRepo = ownerRepository;
            _neighborhoodRepo = neighborhoodRepository;
        }

        // GET: WalksController
        public ActionResult Index()
        {
            //List<Walk> walks = _walkRepo.GetAllWalks();

            //return View(walks);
            return View();
        }

        // GET: WalksController/Details/5
        public ActionResult Details(int id)
        {

            return View();
            //Walker walker = _walkerRepo.GetWalkerById(id);
            //List<Walk> walks = _walkRepo.GetWalksByWalkerId(walker.Id);

            //WalkerProfileViewModel vm = new WalkerProfileViewModel()
            //{
            //    Walker = walker,
            //    Walks = walks
            //};

        //Walk walk = _walkRepo.GetWalkById(id);

            //if (walk == null)
            //{
            //    return NotFound();
            //}

            //return View(vm);
        }

        // GET: WalksController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WalksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalksController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WalksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalksController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WalksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
