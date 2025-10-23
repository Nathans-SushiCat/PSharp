# P# – Eine deutschsprachige Programmiersprache auf Basis von C#

**P#** ist eine experimentelle, C#-basierte Programmiersprache, die vollständig auf geschriebener deutscher Sprache beruht. Ziel ist es, die Programmierlogik und -struktur mit deutschen Schlüsselwörtern, Typen und Ausdrücken ausdrückt – lesbar, verständlich und näher an der natürlichen Schriftsprache.

---

## 🔍 Ziel des Projekts

Dieses Projekt zielt darauf ab:

- eine alternative Syntax auf Deutsch bereitzustellen, die C#-Konstrukte ersetzt,
- die Ausdrucksstärke von C# zu erhalten,
- den Bildungszugang zur Programmierung für deutschsprachige Einsteiger zu erleichtern,
- die Umsetzbarkeit lokalsprachlicher Programmierung zu untersuchen.

---

## 🧠 Konzept

P# ist **keine eigenständige Laufzeitumgebung**, sondern ein **präprozessorbasiertes Sprach-Frontend**. Der deutsche P#-Code wird in regulären C#-Code übersetzt und anschließend mit dem .NET-Compiler kompiliert.

Beispiel (P#):

```psharp

wenn eingabe ist größer als 10 dann
    ausgabe "Die Zahl ist groß".
sonst
    ausgabe "Die Zahl ist klein".
