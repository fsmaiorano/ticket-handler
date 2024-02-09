import { Button } from '@/components/ui/button'
import {
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from '@/components/ui/dialog'

export function CreateTicket() {
  //get sectors of the user holder
  //get priority of the user holder

  return (
    <>
      <DialogHeader>
        <DialogTitle>Create Ticket</DialogTitle>
        <DialogDescription>
          This action cannot be undone. Are you sure you want to permanently
          delete this file from our servers?
        </DialogDescription>
      </DialogHeader>
      <DialogFooter>
        <Button type="submit">Confirm</Button>
      </DialogFooter>
    </>
  )
}
