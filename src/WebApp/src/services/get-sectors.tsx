import { api } from '@/lib/axios'

export interface GetSectorsParams {
  holderId: string
}

export interface GetSectorResponse {
  id: string
  name: string
  holderId: string
  isActive: boolean
  createdAt: string
  updatedAt: string
}

export async function getSectors({ holderId }: GetSectorsParams) {
  const response = await api.get<GetSectorResponse[]>(
    `/api/Sector/holder/${holderId}`,
  )

  return response.data
}
