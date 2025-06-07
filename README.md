# NASA Year on Psyche - Project Capstone
## Setup Development Environment
1. Clone the repository:
```
git clone https://github.com/MissionToPsyche-Iridium/iridium_23g_year_sim-uci.git
```
2. Open `Unity Hub` 
   - "Projects" > "Add" > "Add project from disk"
   - Select newly cloned `iridium_23g_year_sim-uci` folder
   - Open project
3. Run (locally):
```
python3 -m http.server 8000
```
## Notes for future maintainers:
- Psyche is *rotating*. Take this into account when implementing anything to with physical interactions such as gravity.

