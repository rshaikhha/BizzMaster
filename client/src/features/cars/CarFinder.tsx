import { Autocomplete, Box, Divider, TextField, Typography } from "@mui/material";
import { useState, useEffect } from "react";
import agent from "../../app/api/agent";
import { Brand, Platform, Car } from "../../app/models/car"
import CarCard from "./CarCard";

export default function CarFinder() {


    const [brands, setBrands] = useState<Brand[]>([]);
    const [platforms, setPlatforms] = useState<Platform[]>([]);
    const [cars, setCars] = useState<Car[]>([]);
    const [selectedCar, setSelectedCar] = useState<Car | null>(null)

    useEffect(() => {
        agent.Cars.brands().then((res) => setBrands(res))
    }, [])

    const handleSelectBrand = (value: string | undefined) => {
        setCars([])
        setPlatforms([])
        agent.Cars.brandPlatforms(value).then((res) => setPlatforms(res)).catch(()=> setPlatforms([]))
        
    }

    const handleSelectPlatform = (value: string | undefined) => {
        setCars([])
        agent.Cars.platformCars(value).then((res) => setCars(res)).catch(()=> setCars([]))
        
    }

    const handleSelectCar = (value: Car | null) => {
        setSelectedCar(value)
        
    }


    return (
        <Box>
            <Autocomplete
                disabled={brands.length==0}
                disablePortal
                id="brand"
                key={brands.length > 0 ? brands[0].title : 'brands'}
                options={brands}
                getOptionLabel={(option : Brand) => option.title}

                sx={{ width: '80%', mb: 4 }}
                renderInput={(params) => <TextField {...params} label="Brand" />}
                onChange={(event, value) => handleSelectBrand(value?.title)}
            />
            <Autocomplete
                disabled={platforms.length==0}
                disablePortal
                id="platform"
                key={platforms.length > 0 ? platforms[0].title : 'platforms'}
                options={platforms}
                getOptionLabel={(option : Platform) => option.title}

                sx={{ width: '80%', mb: 4 }}
                renderInput={(params) => <TextField {...params} label="Platform" />}
                onChange={(event, value) => handleSelectPlatform(value?.title)}
            />
            <Autocomplete
                disabled={cars.length==0}
                disablePortal
                id="car"
                key={cars.length > 0 ? cars[0].title : 'cars'}
                options={cars}
                getOptionLabel={(option : Car) => option.title}

                sx={{ width: '80%', mb: 4 }}
                renderInput={(params) => <TextField {...params} label="Model" />}
                onChange={(event, value) => handleSelectCar(value)}
            />
            {selectedCar && <CarCard data = {selectedCar} />}
            

        </Box>
    )
}