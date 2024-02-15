import {
    DialogContent,
    DialogDescription,
    DialogHeader,
    DialogTitle,
} from '@/components/ui/dialog'
import { getTicket } from '@/services/get-ticket'
import { useQuery } from '@tanstack/react-query'

export interface TicketDetailProps {
  ticketId: string
  open: boolean
}

export function TicketDetail({ ticketId, open }: TicketDetailProps) {
  const { data: ticket } = useQuery({
    queryKey: ['ticket', ticketId],
    queryFn: () => getTicket({ ticketId }),
    enabled: open,
  })

  return (
    <>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Ticket: {ticketId}</DialogTitle>
          <DialogDescription>Ticket details</DialogDescription>
        </DialogHeader>
        {ticket && (
          <div>
            <p>{ticket.title}</p>
            <p>{ticket.content}</p>
          </div>
        )}
      </DialogContent>
    </>
  )
}
