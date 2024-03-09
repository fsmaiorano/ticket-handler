import { TicketPriority as ImportedTicketPriority } from '@/models/ticket-priority'

interface TicketPriorityProps {
  priority: ImportedTicketPriority
}

const ticketPriorityMap: Record<ImportedTicketPriority, string> = {
  [ImportedTicketPriority.High]: 'High',
  [ImportedTicketPriority.Medium]: 'Medium',
  [ImportedTicketPriority.Low]: 'Low',
}

export function TicketPriority({ priority }: TicketPriorityProps) {
  return (
    <div className="flex items-center gap-2">
      {priority === ImportedTicketPriority.High && (
        <span className="h-2 w-2 rounded-full bg-rose-500" />
      )}

      {priority === ImportedTicketPriority.Low && (
        <span className="h-2 w-2 rounded-full bg-emerald-500" />
      )}

      {priority === ImportedTicketPriority.Medium && (
        <span className="h-2 w-2 rounded-full bg-amber-500" />
      )}

      <span className="font-medium text-muted-foreground">
        {ticketPriorityMap[priority]}
      </span>
    </div>
  )
}