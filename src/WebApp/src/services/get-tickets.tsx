import { api } from '@/lib/axios'
import { Ticket } from '@/models/ticket'

export interface GetTicketsParams {
  holderId: string
}

interface GetTicketsResponse {
  success: boolean
  message: string
  pageNumber: number
  totalPages: number
  items: Ticket[]
}

export async function getTickets({ holderId }: GetTicketsParams) {
  const response = await api.get<GetTicketsResponse>(
    `/api/ticket/holder/${holderId}`,
  )
  return response.data
}
