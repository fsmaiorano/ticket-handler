import { api } from '@/lib/axios'

export interface RegisterHolderRequest {
  holderName: string
  managerName: string
  phone: string
  email: string
}

export async function registerHolder({
  holderName,
  managerName,
  phone,
  email,
}: RegisterHolderRequest) {
  await api.post('/holders', { holderName, managerName, phone, email })
}
