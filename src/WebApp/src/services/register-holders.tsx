import { api } from '@/lib/axios'

export interface RegisterHolderRequest {
    holderName: string
    fullName: string
    password: string
    email: string
}

export async function registerHolder({
    holderName,
    fullName,
    password,
    email,
}: RegisterHolderRequest) {
    await api.post('/api/SignUp', { holderName, fullName, password, email })
}


