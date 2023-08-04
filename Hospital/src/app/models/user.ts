import { Photo } from "./photo"

export interface User {
    id: number
    username: string
    age: number
    token: string
    photo: Photo
}