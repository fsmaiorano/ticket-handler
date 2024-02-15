import { api } from '@/lib/axios'
import { Ticket } from '@/models/ticket'

export interface GetTicketsParams {
  holderId: string
}

export async function getTickets({ holderId }: GetTicketsParams) {
  const response = await api.get<Ticket[]>(`/api/ticket/holder/${holderId}`)

  return response.data
}
