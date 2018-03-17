using System.Threading;
using System.Threading.Tasks;
using coIT.MyHeco.Registrierung.Domain;
using coIT.MyHeco.Registrierung.Domain.Events;
using coIT.MyHeco.Registrierung.Domain.Services;
using MediatR;

namespace coIT.MyHeco.Registrierung.Data
{
    public class BenutzerAktiviertHandler : INotificationHandler<AccountActivated>
    {
        private readonly INichtAktivierteBenutzerRepository _aktivierteBenutzerRepository;
        private readonly MyHecoContext _context;

        public BenutzerAktiviertHandler(MyHecoContext context,
            INichtAktivierteBenutzerRepository aktivierteBenutzerRepository)
        {
            _context = context;
            _aktivierteBenutzerRepository = aktivierteBenutzerRepository;
        }

        public Task Handle(AccountActivated notification, CancellationToken cancellationToken)
        {
            _context.Benutzer.Add(new MyHecoBenutzer
            {
                Email = notification.Email,
                FirmenName = notification.Firma.Name,
                Passwort = notification.Passwort
            });
            _context.SaveChanges();
            var user =
                _aktivierteBenutzerRepository.FindeBenutzerByMail(notification.Email) as NichtAktivierterBenutzer;
            _aktivierteBenutzerRepository.Delete(user);
            _aktivierteBenutzerRepository.Speichern();
            return Task.CompletedTask;
        }
    }
}