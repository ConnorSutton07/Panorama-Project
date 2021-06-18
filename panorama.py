# Connor Sutton | 6/18/21 | panorama.py

import pandas as pd
import numpy as np
from scipy.stats import chi2

def Mahalanobis(x: np.array, data: np.array, cov: np.array = None) -> float:
    """
    Computes the Mahalanobis Distance between each row of x
    and the given data
    
    """
    
    x_std = x - data.mean(axis=0)
    if cov is None:
        cov = np.cov(data.T)
    inv_cov = np.linalg.inv(cov)
    mahal = np.dot(np.dot(x_std, inv_cov), x_std.T)
    return mahal

def NearestPoint(x: np.array, data: np.array) -> int:
    """
    Uses the distance formula to find the nearest 2D point
    in an array to the given point.
    
    Returns the index of this point.
    
    """
    
    closest = [0, np.inf]
    for i in range(data.shape[0]):
        point = data[i]
        dist = np.sqrt( (x[0] - point[0])**2 + (x[1] - point[1])**2 )
        if dist < closest[1]:
            closest[0] = i
            closest[1] = dist
    return closest[0]

def GetSpecies(lat: float, lon: float, kya: str, clouds: dict, bios: list, names: list) -> list:
    """
    Compiles a list of species that may be present
    given an area and time in the American continent
    
    """
    try:
        primary_species = []
        secondary_species = []
        df = clouds[kya]

        loc_data = np.array(list(zip(df['Longitude'].to_numpy(), df['Latitude'].to_numpy())))
        clim_data = np.array(list(zip(df['puntos10'].to_numpy(),
                                      df['puntos11'].to_numpy(),
                                      df['puntos12'].to_numpy())))

        loc = np.array([lon, lat])
        idx = NearestPoint(loc, loc_data)
        climate_point = clim_data[idx]
        print("Closest Point:", loc_data[idx])
        for i in range(len(bios)):
            data = np.array(bios[i])
            mahal = Mahalanobis(climate_point, data)
            if mahal < chi2.ppf((1 - 0.05), df = 2):
                primary_species.append(names[i])
            elif mahal < chi2.ppf((1 - 0.01), df = 2):
                secondary_species.append(names[i])
        return primary_species, secondary_species
    except:
        print("Invalid Input")
        pass

def main(species_data_file: str = 'ValsCoordsPanoramaAlternas3.csv',
         times_data_folder: str =  'HexagonsCoordsVars/') -> None:

    d = pd.read_csv(species_data_file)
    names = set(d.loc[:, 'Species'])

    clouds = {}
    for i in range(3000, 136000, 1000):
        try:
            filename = times_data_folder + 'vars_coords_' + str(i) + '.csv'
            tmp = pd.read_csv(filename)
            clouds[str(i)] = tmp
        except:
            pass

    s = d['Species'].to_numpy()
    bio10 = d['bio_10'].to_numpy()
    bio11 = d['bio_11'].to_numpy()
    bio12 = d['bio_12'].to_numpy()
    lat   = d['latitude'].to_numpy()
    lon   = d['longitude'].to_numpy()
    bio_data = list(zip(bio10, bio11, bio12))
    loc_data = list(zip(lat, lon))

    cur_list = 0
    bios = [[]]
    locs = [[]]
    for i in range(1, s.size):
        if s[i] != s[i-1]:
            cur_list += 1
            bios.append([])
            locs.append([])
        bios[cur_list].append(bio_data[i])
        locs[cur_list].append(loc_data[i])

    correlation_max = 0.975
    tmp_bios = []
    tmp_locs = []
    tmp_names = []
    names_lst = sorted(names)
    for i in range(len(bios)):
        if (names_lst[i] != 'Mazama_americana'): # weird issue with mazama americana
            corr = np.corrcoef(bios[i], rowvar=False)
            if not (len(bios[i]) < 9 or np.abs(corr[0][1]) > correlation_max or np.abs(corr[0][2]) > correlation_max):
                tmp_bios.append(bios[i])
                tmp_locs.append(locs[i])
                tmp_names.append(names_lst[i])

    bios  = tmp_bios
    locs  = tmp_locs
    names = tmp_names

    lat = float(input("Latitude: "))
    lon = float(input("Longitude: "))
    kya = input("Kya: ")

    primary_species, secondary_species = GetSpecies(lat, lon, kya, clouds, bios, names)
    print("+--------------------------+")
    print("|     Primary Species      |")
    print("+--------------------------+\n")
    print(primary_species)
    print('\n\n')
    print("+--------------------------+")
    print("|    Secondary Species     |")
    print("+--------------------------+\n")
    print(secondary_species)


if __name__ == "__main__":
    main()
