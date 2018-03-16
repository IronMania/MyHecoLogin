Funktionalität: WebseitenNutzer
Ein WebseitenNutzer ist jeder der initial auf die Webseite kommt und noch keine Authentifizierung gestartet hat. 
D.h. die Email von dem Nutzer die als Login verwerndet wird ist noch nicht bekannt.
Szenario: Anonymer Nutzer ist unbekannt
	Angenommen ein nicht angemeldeter Benutzer kommt auf die Webseite.
	Wenn er eine Email eingibt, die das System nicht kennt
	Dann wird er zu einem unbekannten Nutzer

Szenario: Anonymer Nutzer ist im Comwork bekannt
	Angenommen ein nicht angemeldeter Benutzer kommt auf die Webseite.
	Wenn er eine Email eingibt, die im Comwork hinterlegt ist
	Dann wird er zu einem ComworkNutzer

Szenario: Anonymer Nutzer ist im MyHeco bekannt
	Angenommen ein nicht angemeldeter Benutzer kommt auf die Webseite.
	Wenn er eine Email eingibt, die im MyHeco hinterlegt ist
	Dann ist er ein MyHecoNutzer
