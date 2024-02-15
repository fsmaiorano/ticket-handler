import { api } from '@/lib/axios'
import { Ticket } from '@/models/ticket'

export interface GetTicketParams {
  ticketId: string
}

export async function getTicket({ ticketId }: GetTicketParams) {
  const response = await api.get<Ticket>(`/api/ticket/${ticketId}`)

  return response.data
}
