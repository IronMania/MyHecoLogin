Funktionalität: MyHecoNutzer
Ein MyHecoNutzer ist ein Gast der bereits logindaten besitzt und sich noch nicht eingeloggt hat.
Szenario: My Heco Nutzer loggt sich ein
	Angenommen ein MyHecoBenutzer wurde erkannt.
	Wenn er das richtige Passwort eingibt
	Dann Ist er ein eingeloggter Benutzer
		Und darf sich ausloggen

Szenario: My Heco Nutzer loggt sich mit dem falschen password ein
	Angenommen ein MyHecoBenutzer wurde erkannt.
	Wenn er das falsche password 1 mal eingibt
	Dann ist er ein MyHecoNutzer
		Und darf sich mithilfe seines Passwortes einloggen
		Und darf sein Passwort zurücksetzen.

Szenario: My Heco Nutzer loggt sich mit dem falschen password zu oft ein
	Angenommen ein MyHecoBenutzer wurde erkannt.
	Wenn er das falsche password 3 mal eingibt
	Dann ist er ein gesperrter Benutzer
		Und darf nichts
