export interface Login {
    userName: string;
    password: string;
}

export interface SignUp {
    userName: string;
    emailAddress: string;
    password: string;
    confirmPassword: string;
}

export interface ProductDetails {
    productId: string;
    productDesc: string;
    forecastedProducedCount: number;
    imageBase64?: string;
}

export interface ResponseDefaultDto {
    message: string;
    statusCode: number;
}

export interface ProductDto {
    productDetails: ProductDetails[];
    responseDefaultDto: ResponseDefaultDto;
}

export interface PartsDetails {
    partsId?: string;
    partsDesc: string;
    quantity: number;
    imageBase64?: string;
    productId: string;
}

export interface PartsDto {
    partsDetails: PartsDetails[];
    responseDefaultDto: ResponseDefaultDto;
}

export interface ProduceDto {
    userName: string;
    productId: string;
    partsId: string[];
    quantity: number;
}

