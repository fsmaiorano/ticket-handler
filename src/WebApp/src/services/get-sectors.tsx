import { api } from '@/lib/axios'

export interface RegisterHolderRequest {
    holderName: string
    fullName: string
    password: string
    email: string
}

export async function get({
    holderName,
    fullName,
    password,
    email,
}: RegisterHolderRequest) {
    await api.post('/api/authentication/signup', { holderName, fullName, password, email })
}


