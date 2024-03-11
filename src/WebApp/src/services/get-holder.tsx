import { api } from '@/lib/axios'
import { Sector } from '@/models/sector'

export interface GetHolderRequest {
  holderId: string
}

//REVIEW - Adjust the response to match the model with base response
export interface GetHolderResponse {
  id: string
  name: string
  sectors: Sector[]
  createdAt: string
  isActive: boolean
}

export async function getHolder({ holderId }: GetHolderRequest) {
  const response = await api.get<GetHolderResponse>(`/api/holder/${holderId}`)

  return response.data
}
