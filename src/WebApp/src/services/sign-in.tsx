import { api } from '@/lib/axios'
import { User } from '@/models/user'

export interface SignInRequest {
  email: string
  password: string
}

export interface SignInResponse {
  success: boolean
  message: string
  redirectUrl: string
  token: string
  user: User
}

export async function signIn({ email, password }: SignInRequest) {
  const response = await api.post<SignInResponse>(
    '/api/authentication/signin',
    { email, password },
  )
  return response.data
}
