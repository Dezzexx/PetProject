namespace Client {
    struct AnimationSwitchEvent {
        public enum AnimationType
        {
            Idle, StayShoot, Win, DefaultRun
        }
        public AnimationType AnimationSwitcher;
    }
}