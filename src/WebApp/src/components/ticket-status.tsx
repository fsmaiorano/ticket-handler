import { TicketStatus as ImportedTicketStatus } from '@/models/ticket-status'

interface TicketStatusProps {
  status: ImportedTicketStatus
}

const ticketStatusMap: Record<ImportedTicketStatus, string> = {
  [ImportedTicketStatus.Open]: 'Open',
  [ImportedTicketStatus.Closed]: 'Closed',
  [ImportedTicketStatus.Active]: 'Active',
}

export function TicketStatus({ status }: TicketStatusProps) {
  return (
    <div className="flex items-center gap-2">
      {status === ImportedTicketStatus.Closed && (
        <span className="h-2 w-2 rounded-full bg-rose-500" />
      )}

      {status === ImportedTicketStatus.Open && (
        <span className="h-2 w-2 rounded-full bg-emerald-500" />
      )}

      {status === ImportedTicketStatus.Active && (
        <span className="h-2 w-2 rounded-full bg-amber-500" />
      )}

      <span className="font-medium text-muted-foreground">
        {ticketStatusMap[status]}
      </span>
    </div>
  )
}
