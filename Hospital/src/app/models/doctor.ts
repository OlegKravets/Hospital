import { Photo } from "./photo"

export interface Doctor {
    id: number
    name: string
    age: number
    salary: number
    token: string
    hospitalName: number
    hospitalAddress: string
    photo: Photo
}