import { api } from '@/lib/axios'
import { Ticket } from '@/models/ticket'

export interface UpdateTicketRequest {
  title: string
  content: string
  status: number
  priority: number
  userId: string
  holderId: string
  sectorId: string
}

export interface UpdateTicketResponse {
  message: string
  success: boolean
  ticket: Ticket
}

export async function updateTicket(request: UpdateTicketRequest) {
  const response = await api.put<UpdateTicketResponse>(
    `/api/ticket`,
    request,
  )

  return response.data
}
