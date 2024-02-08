import { api } from '@/lib/axios'

export interface SignInRequest {
  email: string
  password: string
}

export interface SignInResponse {
  token: string
  redirectUrl: string
}

export async function signIn({ email, password }: SignInRequest) {
  const response = await api.post<SignInResponse>(
    '/api/authentication/signin',
    { email, password },
  )
  return response.data
}
