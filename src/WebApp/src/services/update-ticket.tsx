import { api } from '@/lib/axios'
import { Ticket } from '@/models/ticket'

export interface UpdateTicketRequest {
  id: string
  title: string
  content: string
  status: string
  priority: string
  userId: string
  holderId: string
  sectorId: string,
  assigneeId?: string | null
}

export interface UpdateTicketResponse {
  message: string
  success: boolean
  ticket: Ticket
}

export async function updateTicket(request: UpdateTicketRequest) {
  const response = await api.put<UpdateTicketResponse>(
    `/api/ticket/${request.id}`,
    request,
  )

  return response
}
