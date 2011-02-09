MY NOTES Conversation Handlers
===========================

This is a demonstration of opinionated multiple Aggregate communication in CQRS.
ConversationHandlers are created just like EventHandlers, they also subscribe to the Event raised by Domain.
Then in Conversation Handler we can write code that we need to converse between 2 AR's.
This can also be called as a simple stateless saga. It does not use NSB simple InProcessEventBus is used.
In this application Summary AR for Notes is created.
But can be used to Create or Update follow-up Aggregates.

