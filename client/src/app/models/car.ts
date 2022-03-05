export interface Brand {
    title: string
    countryName: string
    countryAbbr: string
    logoImage: string
    countryFlagImage: string
    active: boolean
    isOpen: boolean 
}

export interface Platform {
    title: string
    brandTitle: string
    countryName: string
    countryAbbr: string
    brandLogoImage: string
    countryFlagImage: string
    active: boolean
    isOpen: boolean
}

export interface Car {
    title: string
    platformTitle: string
    brandTitle: string
    countryName: string
    countryAbbr: string
    brandLogoImage: string
    countryFlagImage: string
    active: boolean
}