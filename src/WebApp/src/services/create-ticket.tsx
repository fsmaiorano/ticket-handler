import { api } from '@/lib/axios'
import { Ticket } from '@/models/ticket'

export interface CreateTicketRequest {
  title: string
  content: string
  status: string
  priority: string
  userId: string
  holderId: string
  sectorId: string
}

export interface CreateTicketResponse {
  message: string
  success: boolean
  ticket: Ticket
}

export async function createTicket(request: CreateTicketRequest) {
  const response = await api.post<CreateTicketResponse>(
    `/api/ticket`,
    request,
  )

  return response.data
}
