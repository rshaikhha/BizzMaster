
export interface Product {
    id: number;
    title: string;
    partNumber: string;
    description: string;
    brand: string;
    category: string;
    itemVolume: number;
    itemWeight: number;
    itemPerSet: number;
    order: number;

}

export interface ProductParams {
    orderBy: string;
    searchTerm?: string;
    types?: string[];
    brands?: string[];
    pageNumber: number;
    pageSize: number;
}