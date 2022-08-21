# BlogApp-Backend

Build a minimal Blog Engine / CMS backend API, that allows to create, edit and publish text-
based posts, with an approval flow where three different user types may interact.

The API should allow the following operations for the specified roles:

• Retrieve a list of all published posts (all roles)

• Add comments to a published post (all roles)

• Get own posts, create and edit posts (Writer)

• Submit posts (the post should move to a “pending approval” status where it’s locked and cannot be updated) (Writer)

• Get, Approve or Reject pending posts (Editor)
