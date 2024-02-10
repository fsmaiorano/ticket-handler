import { Button } from '@/components/ui/button'
import {
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from '@/components/ui/dialog'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
import {
  Select,
  SelectContent,
  SelectGroup,
  SelectItem,
  SelectLabel,
  SelectTrigger,
  SelectValue,
} from '@/components/ui/select'
import { Textarea } from '@/components/ui/textarea'
import { TicketPriority } from '@/models/ticket-priority'

export function CreateTicket() {
  //get sectors of the user holder
  //get priority of the user holder

  return (
    <>
      <DialogHeader>
        <DialogTitle>Create Ticket</DialogTitle>
        <DialogDescription>
          <div className="mt-5 space-y-3">
            <Label htmlFor="email">Subject</Label>
            <Input id="email" type="email" value="admin@tickethandler.com" />
          </div>
          <div className="mt-5 space-y-3">
            <Label htmlFor="email">Message</Label>
            <Select>
              <SelectTrigger className="w-[180px]">
                <SelectValue placeholder="Select the priority" />
              </SelectTrigger>
              <SelectContent>
                <SelectGroup>
                  <SelectLabel>Priorities</SelectLabel>
                  {Object.keys(TicketPriority)
                    .filter((value) => isNaN(Number(value)))
                    .map((priority) => {
                      return (
                        <SelectItem key={priority} value={priority}>
                          {priority}
                        </SelectItem>
                      )
                    })}
                </SelectGroup>
              </SelectContent>
            </Select>
          </div>
          <div className="mt-5 space-y-3">
            <Label htmlFor="email">Message</Label>
            <Textarea id="email" value="admin@tickethandler.com" />
          </div>
        </DialogDescription>
      </DialogHeader>
      <DialogFooter>
        <Button type="submit">Confirm</Button>
      </DialogFooter>
    </>
  )
}
